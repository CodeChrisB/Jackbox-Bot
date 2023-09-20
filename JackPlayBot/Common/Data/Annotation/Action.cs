using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JackPlayBot.Common.Data.Annotation
{
    internal class BotAction : Attribute
    {
        public string State { get; } = "";

        public BotAction(string state)
        {
            State = state;
        }

    }
}
