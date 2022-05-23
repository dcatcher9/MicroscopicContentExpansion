﻿using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;

namespace MicroscopicContentExpansion.NewComponents {
    [AllowedOn(typeof(BlueprintUnitFact))]
    public class CriticalConfirmationUnarmed :
    UnitFactComponentDelegate,
    IInitiatorRulebookHandler<RuleAttackRoll>,
    IRulebookHandler<RuleAttackRoll>,
    ISubscriber,
    IInitiatorRulebookSubscriber {
        public int Bonus;

        public void OnEventAboutToTrigger(RuleAttackRoll evt) {
            if (!evt.Weapon.IsMonkUnarmedStrike)
                return;
            evt.CriticalConfirmationBonus += Bonus;
        }

        public void OnEventDidTrigger(RuleAttackRoll evt) {
        }
    }
}
