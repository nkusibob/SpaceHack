//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace app_HackSpace.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Deliver
    {
        public int idDeliver { get; set; }
        public string title { get; set; }
        public string descDeliver { get; set; }
        public string logo { get; set; }
        public int idHackathon { get; set; }
    
        public virtual Hackathon Hackathon { get; set; }
        public virtual Hackathon Hackathon1 { get; set; }
    }
}
