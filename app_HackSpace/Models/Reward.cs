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
    
    public partial class Reward
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Reward()
        {
            this.has_reward = new HashSet<has_reward>();
        }
    
        public int idReward { get; set; }
        public string nameReward { get; set; }
        public string descReward { get; set; }
        public bool IsSelected { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<has_reward> has_reward { get; set; }
    }
}
