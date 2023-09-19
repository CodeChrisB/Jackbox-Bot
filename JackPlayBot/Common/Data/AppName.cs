using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JackPlayBot.Common.Data
{
    public class AppName
    {
        //When Making your Bot add the appName and enum  of the game u
        //created the bot for here so the bot accepts the game and sends you data
        private static Dictionary<string, Games> AppNameDictionary = new Dictionary<string, Games>()
        {
            { "PollPosition",Games.Guesspionage},
        };

        public static Games? toGameEnum(string appName)
        {
            appName = appName.ToLower();
            if (!AppNameDictionary.ContainsKey(appName)) return null;

            return AppNameDictionary[appName];
        }
    }
}



/*
 Todo add all AppNames

got them from this cool repo
https://github.com/Pinball3D/jackboxgamesapi/blob/main/appTags.py


"quiplash2-international"
"guesspionage-crowdplay"
"drawful2"
"drawful2international"
"acquisitions-inc"
"ydkj2015"
"drawful"
"wordspud"
"lieswatter"
"fibbage"
"fibbage2"
"earwax"
"auction"
"bombintern"
"quiplash"
"fakinit"
"awshirt"
"quiplash2"
"triviadeath"
"pollposition"
"fibbage3"
"survivetheinternet"
"monstermingle"
"bracketeering"
"overdrawn"
"ydkj2018"
"splittheroom"
"rapbattle"
"slingshoot"
"patentlystupid"
"triviadeath2"
"rolemodels"
"jokeboat"
"ridictionary"
"pushthebutton"
"jackbox-talks"
"quiplash3"
"everyday"
"worldchamps"
"blanky-blank"
"apply-yourself"
"drawful-animate"
"the-wheel"
"survey-bomb"
"murder-detectives"
"quiplash3-tjsp"
"awshirt-tjsp"
"triviadeath2-tjsp"
"fourbage"
"htmf"
"antique-freak"
"range-game"
"lineup"
  
*/