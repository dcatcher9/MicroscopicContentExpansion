﻿using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.ResourceLinks;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.Utility;
using TabletopTweaks.Core.Utilities;
using static MicroscopicContentExpansion.Base.Main;

namespace MicroscopicContentExpansion.Base.NewContent.AntipaladinFeatures {
    internal class AuraofCowardice {
        public static void AddAuraOfCowardiceFeature() {

            var AOCIcon = BlueprintTools.GetBlueprint<BlueprintAbility>("d2aeac47450c76347aebbc02e4f463e0").Icon;
            var AuraOfCowardiceEffectBuff = Helpers.CreateBlueprint<BlueprintBuff>(MCEContext, "AntipaladinAuraOfCowardiceEffectBuff", bp => {

                bp.SetName(MCEContext, "Aura of Cowardice Debuff");
                bp.SetDescription(MCEContext, "At 3rd level, an antipaladin radiates a palpably daunting aura that causes all enemies" +
                    " within 10 feet to take a –4 penalty on saving throws against fear effects. Creatures that are normally immune to" +
                    " fear lose that immunity while within 10 feet of an antipaladin with this ability. This ability functions only" +
                    " while the antipaladin remains conscious, not if he is unconscious or dead.");

                bp.m_Icon = AOCIcon;
                bp.AddComponent<SavingThrowBonusAgainstDescriptor>(c => {
                    c.SpellDescriptor = SpellDescriptor.Fear;
                    c.ModifierDescriptor = Kingmaker.Enums.ModifierDescriptor.UntypedStackable;
                    c.Value = -4;
                    c.Bonus = new ContextValue {
                        ValueType = ContextValueType.Simple,
                        Value = 0,
                        ValueRank = Kingmaker.Enums.AbilityRankType.Default,
                        Property = Kingmaker.UnitLogic.Mechanics.Properties.UnitProperty.None
                    };
                });
                bp.AddComponent<AbilityAreaEffectRunAction>(c => {
                    c.Round = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            SpellDescriptor = SpellDescriptor.FearImmunity
                        });
                });
                bp.Frequency = DurationRate.Rounds;
                bp.IsClassFeature = true;
            });


            var AuraOfCowardiceArea = Helpers.CreateBlueprint<BlueprintAbilityAreaEffect>(MCEContext, "AntipaladinAuraOfCowardiceArea", bp => {
                bp.AggroEnemies = true;
                bp.AffectEnemies = true;
                bp.m_TargetType = BlueprintAbilityAreaEffect.TargetType.Enemy;
                bp.Shape = AreaEffectShape.Cylinder;
                bp.Size = new Feet() { m_Value = 13 };
                bp.Fx = new PrefabLink();
                bp.AddComponent<AbilityAreaEffectBuff>(c => {
                    c.m_Buff = AuraOfCowardiceEffectBuff.ToReference<BlueprintBuffReference>();
                    c.Condition = new ConditionsChecker() {
                        Conditions = new Condition[] {
                            new ContextConditionIsEnemy()
                        }
                    };
                });
            });

            var AuraOfCowardiceBuff = Helpers.CreateBlueprint<BlueprintBuff>(MCEContext, "AntipaladinAuraOfCowardiceBuff", bp => {
                bp.SetName(MCEContext, "Aura of Cowardice");
                bp.SetDescription(MCEContext, "At 3rd level, an antipaladin radiates a palpably daunting aura that causes all enemies" +
                    " within 10 feet to take a –4 penalty on saving throws against fear effects. Creatures that are normally immune to" +
                    " fear lose that immunity while within 10 feet of an antipaladin with this ability. This ability functions only" +
                    " while the antipaladin remains conscious, not if he is unconscious or dead.");
                bp.m_Icon = AOCIcon;

                bp.IsClassFeature = true;
                bp.AddComponent<AddAreaEffect>(c => {
                    c.m_AreaEffect = AuraOfCowardiceArea.ToReference<BlueprintAbilityAreaEffectReference>();
                });
            });

            var AuraOfCowardiceFeature = Helpers.CreateBlueprint<BlueprintFeature>(MCEContext, "AntipaladinAuraOfCowardiceFeature", bp => {
                bp.SetName(MCEContext, "Aura of Cowardice");
                bp.SetDescription(MCEContext, "At 3rd level, an antipaladin radiates a palpably daunting aura that causes all enemies" +
                    " within 10 feet to take a –4 penalty on saving throws against fear effects. Creatures that are normally immune to" +
                    " fear lose that immunity while within 10 feet of an antipaladin with this ability. This ability functions only" +
                    " while the antipaladin remains conscious, not if he is unconscious or dead.");
                bp.m_Icon = AOCIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AuraFeatureComponent>(c => {
                    c.m_Buff = AuraOfCowardiceBuff.ToReference<BlueprintBuffReference>();
                });
            });
        }
    }
}