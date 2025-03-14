using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Auths
{
    public class AdminUser : UserBase
    {

        public Guid AdminGuidNumber { get; set; } = Guid.NewGuid();

        public AdminRole Role { get; set; } = AdminRole.StandardAdmin; 

        public bool IsActive { get; set; } = true; 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("CreatedByAdmin")]
        public int? CreatedByAdminId { get; set; } 
        public virtual AdminUser CreatedByAdmin { get; set; } 

        [ForeignKey("UpdatedByAdmin")]
        public int? UpdatedByAdminId { get; set; } 
        public virtual AdminUser UpdatedByAdmin { get; set; } 
    }

    public enum AdminRole
    {
        SuperAdmin = 1,  
        StandardAdmin = 2 
    }
}
