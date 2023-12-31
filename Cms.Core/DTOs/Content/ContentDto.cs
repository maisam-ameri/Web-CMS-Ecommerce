using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Core.DTOs.Content
{
    public class ContentDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
                     
        public string? ShortDescription { get; set; }

        public string? ImageName { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
