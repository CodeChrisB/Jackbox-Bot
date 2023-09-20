using JackPlayBot.Behaviours.Pack3;
using JackPlayBot.Common.Data;
using JackPlayBot.Common.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JackPlayBot
{
    public class PlayBotSetup
    {
        public static void SetUp()
        {
            RegisterBehaviours();
        }

        private static void RegisterBehaviours()
        {
            PBBehaviour.Register<GuessiponageBehaviour>(Games.Guesspionage);
        }
    }
}
