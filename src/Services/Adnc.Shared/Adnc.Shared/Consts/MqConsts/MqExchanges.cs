using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Shared.Consts.MqConsts
{
    public static class MqExchanges
    {
        public const string Logs = "ex-adnc-logs";
        public const string Sms = "ex-adnc-sms";
        public const string Emails = "ex-adnc-emails";
        public const string Dead = "ex-adnc-dead-letter";
    }
}
