using System.Collections.Generic;
using System.Linq;
using Kyoo.Models.Attributes;

namespace Kyoo.Models
{
	public class GenreDE : Genre
	{
		[SerializeIgnore] [NotMergable] public virtual ICollection<GenreLink> Links { get; set; }

		[ExpressionRewrite(nameof(Links), nameof(GenreLink.Child))]
		[SerializeIgnore] [NotMergable] public override ICollection<Show> Shows
		{
			get => Links?.Select(x => x.Parent).ToList();
			set => Links = value?.Select(x => new GenreLink(x, this)).ToList();
		}

		public GenreDE() {}

		public GenreDE(Genre item)
		{
			Utility.Assign(this, item);
		}
	}
}