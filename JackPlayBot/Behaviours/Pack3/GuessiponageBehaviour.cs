using JackPlayBot.Common.Data.Annotation;
using JackPlayBot.Common.Register;
using JackPlayBot.Common.Register.ActionCtx.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JackPlayBot.Behaviours.Pack3
{
    internal class GuessiponageBehaviour : CommonBehaviour
    {

        [BotAction("Lobby")]
        public async Task OnSeeLogo(ActionContext actionContext)
        {
            Console.WriteLine("Im registerd and I trigger");
        }
    }
}
