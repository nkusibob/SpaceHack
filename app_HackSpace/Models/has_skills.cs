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
    
    public partial class has_skills
    {
        public int idhas_skills { get; set; }
        public int id_Profile { get; set; }
        public string name_skills { get; set; }
    
        public virtual C_Profile C_Profile { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
