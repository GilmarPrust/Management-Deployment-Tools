using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCM.Core.Entities.secondary
{
    public class Filter : BaseEntity
    {
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(240)]
        public string Description { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string FilterCommand { get; set; } = string.Empty;


        public virtual ICollection<Application> Applications { get; set; } = new HashSet<Application>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Filter"/> class.
        /// </summary>
        public Filter() { }
    }
}
