using System.ComponentModel.DataAnnotations;
using ITranslateTrainer.Domain.Common;
using ITranslateTrainer.Domain.Enums;

namespace ITranslateTrainer.Domain.Entities
{
    public class Text : IntIdEntity
    {
        [MaxLength(50)] public string String { get; set; } = string.Empty;
        public Language Language { get; set; }
        public bool CanBeOption { get; set; }
        public bool CanBeTested { get; set; }
    }
}
