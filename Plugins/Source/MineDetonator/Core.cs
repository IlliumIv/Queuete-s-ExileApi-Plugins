using System;
using System.Collections.Generic;
using System.Linq;
using ExileCore;
using ExileCore.PoEMemory.Components;
using ExileCore.PoEMemory.MemoryObjects;
using ExileCore.Shared.Enums;
using SharpDX;

namespace MineDetonator
{
    public class Core : BaseSettingsPlugin<Settings>
    {
        private DateTime LastDetonTime;

        public override void Render()
        {
            if (!GameController.InGame)
                return;
            var actor = GameController.Player.GetComponent<Actor>();
            var deployedObjects = actor.DeployedObjects.Where(x => x.Entity != null && x.Entity.Path.Contains("Metadata/MiscellaneousObjects/RemoteMine")).ToList();

            var realRange = Settings.DetonateDist.Value;
            var mineSkill = actor.ActorSkills.Find(x => x.Name.ToLower().Contains("mine"));
            if (mineSkill != null)
            {
                if (mineSkill.Stats.TryGetValue(GameStat.TotalSkillAreaOfEffectPctIncludingFinal, out var areaPct))
                {
                    realRange += realRange * areaPct / 100f;
                    Settings.CurrentAreaPct.Value = realRange;
                }
                else
                {
                    Settings.CurrentAreaPct.Value = 100;
                }
            }
            else
            {
                Settings.CurrentAreaPct.Value = 0;
            }


            if (deployedObjects.Count == 0)
            {
                return;
            }

            var playerPos = GameController.Player.GridPos;

            var nearMonsters = GameController.EntityListWrapper.ValidEntitiesByType[EntityType.Monster].Where(
                x => x != null
                && x.HasComponent<Monster>()
                && x.IsHostile
                && x.IsAlive
                && !x.Path.StartsWith("Metadata/Monsters/LeagueBetrayal/BetrayalTaserNet")
                && !x.Path.StartsWith("Metadata/Monsters/LeagueBetrayal/BetrayalUpgrades/UnholyRelic")
                && !x.Path.StartsWith("Metadata/Monsters/LeagueBetrayal/BetrayalUpgrades/BetrayalDaemonSummonUnholyRelic")
                && x.HasComponent<Buffs>()
                && !x.GetComponent<Buffs>().HasBuff("hidden_monster")
                && !x.GetComponent<Buffs>().HasBuff("avarius_statue_buff")
                && !x.GetComponent<Buffs>().HasBuff("hidden_monster_disable_minions")
                && x.HasComponent<Actor>()
                && FilterNullAction(x.GetComponent<Actor>())
                && x.GetComponent<Actor>().CurrentAction?.Skill?.Name != "AtziriSummonDemons"
                && x.GetComponent<Actor>().CurrentAction?.Skill?.Id != 728
                && Vector2.Distance(playerPos, x.GridPos) < realRange).ToList();

            if (nearMonsters.Count == 0)
                return;

            Settings.TriggerReason = "Path: " + nearMonsters[0].Path;

            if (Settings.Debug.Value)
                LogMessage($"Ents: {nearMonsters.Count}. Last: {nearMonsters[0].Path}", 2);

            if ((DateTime.Now - LastDetonTime).TotalMilliseconds > Settings.DetonateDelay.Value)
            {
                if (deployedObjects.Any(x => x.Entity != null && x.Entity.IsValid /*&& x.Entity.GetComponent<Stats>().StatDictionary[GameStat.CannotDie] == 0*/))
                {
                    if (!(Keyboard.IsKeyDown(Keyboard.VK_RBUTTON) && Settings.CheckMouseKey.Value))
                    {
                        LastDetonTime = DateTime.Now;
                        Keyboard.KeyPress(Settings.DetonateKey.Value);
                    }
                }
            }
        }

        #region Overrides of BasePlugin

        private bool FilterNullAction(Actor actor)
        {
            if (Settings.FilterNullAction.Value)
                return actor.CurrentAction != null;

            return true;
        }

        #endregion
    }
}
