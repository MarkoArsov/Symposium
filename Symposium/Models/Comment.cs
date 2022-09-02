using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Symposium.Models
{
    public class Comment
    {
        public int CommentId { get; set; }

        public int PostId { get; set; }

        public string UserName { get; set; }

        public string Content { get; set; }
    }
}