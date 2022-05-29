using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{
    public static partial class RouterOutputLockStrings
    {

        public static string Get(RouterOutputLockType type, Variant variant)
        {
            StringCollection collection = type switch
            {
                RouterOutputLockType.Lock => Lock,
                RouterOutputLockType.Protect => Protect,
                _ => Unknown
            };
            return collection[variant];
        }

        public static string GetDo(RouterOutputLockType type, RouterOutputLockOperationType operation, bool uppercase) => Get(type, GetDoVariantByOperation(operation, uppercase));
        public static string GetDoing(RouterOutputLockType type, RouterOutputLockOperationType operation, bool uppercase) => Get(type, GetDoingVariantByOperation(operation, uppercase));
        public static string GetDone(RouterOutputLockType type, RouterOutputLockOperationType operation, bool uppercase) => Get(type, GetDoneVariantByOperation(operation, uppercase));

        public static string GetString(this RouterOutputLockType type, Variant variant) => Get(type, variant);
        public static string GetDoString(this RouterOutputLockType type, RouterOutputLockOperationType operation, bool uppercase) => GetDo(type, operation, uppercase);
        public static string GetDoingString(this RouterOutputLockType type, RouterOutputLockOperationType operation, bool uppercase) => GetDoing(type, operation, uppercase);
        public static string GetDoneString(this RouterOutputLockType type, RouterOutputLockOperationType operation, bool uppercase) => GetDone(type, operation, uppercase);

        public static Variant GetDoVariantByOperation(RouterOutputLockOperationType operationType, bool uppercase)
        {
            Variant variantByOperation = operationType switch
            {
                RouterOutputLockOperationType.Lock => Variant.DoLowercase,
                RouterOutputLockOperationType.Unlock => Variant.UndoLowercase,
                RouterOutputLockOperationType.ForceUnlock => Variant.ForceUndoLowercase,
                _ => Variant.UnknownLowercase
            };
            if (uppercase)
                variantByOperation += 1;
            return variantByOperation;
        }

        public static Variant GetDoingVariantByOperation(RouterOutputLockOperationType operationType, bool uppercase)
            => GetDoVariantByOperation(operationType, uppercase) + 2;

        public static Variant GetDoneVariantByOperation(RouterOutputLockOperationType operationType, bool uppercase)
            => GetDoVariantByOperation(operationType, uppercase) + 4;

    }
}
