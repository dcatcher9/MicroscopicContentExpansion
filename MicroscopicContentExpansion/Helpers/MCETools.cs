﻿using Kingmaker.ElementsSystem;
using TabletopTweaks.Core.Utilities;

namespace MicroscopicContentExpansion.Base.MCEHelpers {
    public static class MCETools {

        public static ActionList DoNothing() => Helpers.CreateActionList();

        public static ActionList DoSingle<T>(System.Action<T> init = null) where T : GameAction, new() {
            var t = new T();
            init?.Invoke(t);
            return Helpers.CreateActionList(t);
        }

        public static ConditionsChecker IfSingle<T>(System.Action<T> init = null) where T : Condition, new() {
            var t = new T();
            init?.Invoke(t);
            return new ConditionsChecker() {
                Conditions = new Condition[] { t }
            };
        }

        public static ConditionsChecker IfMany(params Condition[] conditions) {
            return new ConditionsChecker() {
                Conditions = conditions,
                Operation = Operation.And
            };
        }

        public static ConditionsChecker EmptyCondition() {
            return new ConditionsChecker() {
                Conditions = new Condition[0]
            };
        }
    }
}