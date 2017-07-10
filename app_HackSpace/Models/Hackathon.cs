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
    
    public partial class Hackathon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Hackathon()
        {
            this.Delivers = new HashSet<Deliver>();
            this.Delivers1 = new HashSet<Deliver>();
            this.Explores = new HashSet<Explore>();
            this.Explores1 = new HashSet<Explore>();
            this.has_expert = new HashSet<has_expert>();
            this.has_goodie = new HashSet<has_goodie>();
            this.has_participant = new HashSet<has_participant>();
            this.has_reward = new HashSet<has_reward>();
            this.has_sponsor = new HashSet<has_sponsor>();
            this.Launches = new HashSet<Launch>();
            this.Launches1 = new HashSet<Launch>();
            this.Pitches = new HashSet<Pitch>();
            this.Pitches1 = new HashSet<Pitch>();
        }
    
        public int idHackathon { get; set; }
        public string nameHack { get; set; }
        public string UserName { get; set; }
        public string image_ { get; set; }
        public int idTheme { get; set; }
        public int idProg { get; set; }
        public int idLoc { get; set; }
        public int idFacilitator { get; set; }
        public Nullable<int> idPayment { get; set; }
    
        public virtual Facilitator Facilitator { get; set; }
        public virtual Facilitator Facilitator1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Deliver> Delivers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Deliver> Delivers1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Explore> Explores { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Explore> Explores1 { get; set; }
        public virtual Location_ Location_ { get; set; }
        public virtual Location_ Location_1 { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual Payment Payment1 { get; set; }
        public virtual Program Program { get; set; }
        public virtual Program Program1 { get; set; }
        public virtual Theme Theme { get; set; }
        public virtual Theme Theme1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<has_expert> has_expert { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<has_goodie> has_goodie { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<has_participant> has_participant { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<has_reward> has_reward { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<has_sponsor> has_sponsor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Launch> Launches { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Launch> Launches1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pitch> Pitches { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pitch> Pitches1 { get; set; }
    }
}
