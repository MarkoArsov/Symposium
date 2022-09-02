using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Symposium.Models
{
    public class Post
    {
        public int PostId { get; set; }

        public string UserName { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int Likes { get; set; }

        public string ImgURL { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual List<Comment> Comments { get; set; }

        public string LikedBy { get; set; }
    }
}