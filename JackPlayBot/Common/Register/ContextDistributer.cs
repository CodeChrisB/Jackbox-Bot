using JackPlayBot.Common.Data;
using JackPlayBot.Common.Register.ActionCtx.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JackPlayBot.Common.Register
{
    internal class ContextDistributer
    {
        //A Dictionary with the enum key of the game
        //The Value is a Dictonary with the key of the status code and value is the function that gets called 
        private static Dictionary<Games, Dictionary<string, Action<ActionContext>>> BehaviourData = new();


        public static void AddBehaviour(Games game,string statusCode, Action<ActionContext> func)
        {
            //Behaviour for game does not exist
            if (!BehaviourData.ContainsKey(game))
            {
                BehaviourData.Add(game,new Dictionary<string, Action<ActionContext>>());
            }

            BehaviourData[game].Add(statusCode, func);
        }


        public static void CallFunction(ActionContext context, Games game)
        {
            if (!BehaviourData.ContainsKey(game)) return;
            if (!BehaviourData[game].ContainsKey(context.state)) return;

            //Error: Non-static method requires a target.
            BehaviourData[game][(context.state)].Invoke(context);

        }

    }
}
