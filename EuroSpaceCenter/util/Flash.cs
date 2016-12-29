using System;

namespace EuroSpaceCenter.util {
    internal class Flash {
        internal static void Set(System.Web.Mvc.TempDataDictionary tempData, string v) {
            tempData.Remove("message");
            tempData.Add(key: "message", value: v);
        }
    }
}