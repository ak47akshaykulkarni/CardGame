using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Model
{
    public class CardAttributes
    {
        ///ScoresAttributes
        ///OverallScore
        ///Loyalty
        ///Cunningness
        ///Combat
        ///Honour
        ///Brutality
        ///Wisdom
        public int Id               { get; set; }
        public string Title         { get; set; }
        public string Description   { get; set; }
        public string DisplayImage { get; set; }
        
        public int OverallRank      { get; set; }
        public int LoyaltyScore     { get; set; }
        public int CunningnessScore { get; set; }
        public int CombatScore      { get; set; }
        public int HonourScore      { get; set; }
        public int BrutalityScore   { get; set; }
        public int WisdomScore      { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="title"></param>
        /// <param name="desc"></param>
        /// <param name="Overall"></param>
        /// <param name="Loyalty"></param>
        /// <param name="Cunningness"></param>
        public CardAttributes(int id,string title,string desc, int Overall,
            int Loyalty, int Cunningness, int Combat, string Display  /*,int Honour, int Brutality, int Wisdom*/)
        {
            this.Id                 = id        ;
            this.Title              =title      ;
            this.Description        =desc       ;
            this.OverallRank        =Overall    ;
            this.LoyaltyScore       =Loyalty    ;
            this.CunningnessScore   =Cunningness;
            this.DisplayImage       = Display   ;
            this.CombatScore        = Combat    ;
            //this.HonourScore        =Honour     ;
            //this.BrutalityScore     =Brutality  ;
            //this.WisdomScore        = Wisdom    ;
        }                                                     
                                                              
    }                                                         
}                                                                
