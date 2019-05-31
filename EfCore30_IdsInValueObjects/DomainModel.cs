using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfCore30_IdsInValueObjects
{
    public class Blog : IEquatable<Blog>
    {
        //für EF core
        private Blog() { }

        public Blog(BlogId blogId)
        {
            Id = blogId;
        }
        public BlogId Id { get; private set; }

        public bool Equals(Blog other)
        {
            return Id.Value == other?.Id.Value;
        }
    }

    public class Post : IEquatable<Post>
    {
        //für EF core
        private Post() { }

        public Post(PostId postId, BlogId blogId)
        {
            Id = postId;
            BlogId = blogId;
        }
        public BlogId BlogId { get; private set; }
        public PostId Id { get; private set; }

        public bool Equals(Post other)
        {
            return Id.Value == other?.Id.Value;
        }
    }

    public class BlogId : IEquatable<BlogId>
    {
        //für EF core
        private BlogId() { }
        public BlogId(string value)
        {
            Value = value;
        }
        public string Value { get; private set; }

        public bool Equals(BlogId other)
        {
            return Value == other.Value;
        }
    }

    public class PostId : IEquatable<PostId>
    {
        private PostId() { }
        public PostId(string value)
        {
            Value = value;
        }
        public string Value { get; private set; }

        public bool Equals(PostId other)
        {
            return Value == other.Value;
        }
    }
}
