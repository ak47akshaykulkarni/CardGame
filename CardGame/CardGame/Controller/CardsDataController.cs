using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Controller
{
    public static class CardsDataController
    {
        public static void CreateAllCards()
        {
            //Acr.UserDialogs.UserDialogs.Instance.ShowLoading();
            List<Model.CardAttributes> AllCards = new List<Model.CardAttributes>()
            {
                new Model.CardAttributes(0,"Daenerys Targareyn","Daenerys Targareyn"    ,6,5,5,1,"https://68.media.tumblr.com/avatar_44a52b705601_128.png"),
                new Model.CardAttributes(1,"Tyrion Lannister","Tyrion Lannister"        ,5,4,9,5,"http://pm1.narvii.com/6057/f1791c414767bf8e5c7b7f7b5d3b1a7ab54ccd21_128.jpg"),
                new Model.CardAttributes(2,"Jon Snow","Jon Snow"                        ,7,7,3,8,"http://pm1.narvii.com/6500/4e07b86da714f50070746656328a9812ce43f9c6_128.jpg"),
                new Model.CardAttributes(3,"Arya Stark","Arya Stark"                    ,6,4,1,7,"http://pm1.narvii.com/6125/107c3018e2df08eb77d8906329a1ff612a5b4cc7_128.jpg"),
                new Model.CardAttributes(4,"Little Finger","Little Finger"              ,1,7,7,6,"https://68.media.tumblr.com/avatar_68df2762013f_128.png"),
                new Model.CardAttributes(5,"Sansa Stark","Sansa Stark"                  ,9,8,9,3,"https://68.media.tumblr.com/avatar_6fdea35ce350_128.png"),
                new Model.CardAttributes(6,"Ned Stark","Ned Stark"              ,3,9,6,7,"https://68.media.tumblr.com/avatar_ae8d71bd8b7e_128.png"),
                new Model.CardAttributes(7,"Joffrey Baratheon","Joffrey Baratheon"      ,0,2,1,4,"https://68.media.tumblr.com/avatar_dd5d712f5828_128.png"),
                new Model.CardAttributes(8,"Ramsay Bolton","Ramsay Bolton"              ,4,3,5,7,"http://68.media.tumblr.com/avatar_d40d50f3e127_128.png"),
                new Model.CardAttributes(9,"Khal Drogo","Khal Drogo"                    ,3,0,8,2,"http://pm1.narvii.com/6150/3234fbfc187cba1b1d274f5a199416cc8537275d_128.jpg"),
            };
            Model.Constants.ListOFCards = AllCards;
        }
    }
}
