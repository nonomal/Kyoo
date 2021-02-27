using System.Collections.Generic;
using System.Linq;
using Kyoo.Models.Attributes;

namespace Kyoo.Models
{
	public class CollectionDE : Collection
	{
		[SerializeIgnore] [NotMergable] public virtual ICollection<CollectionLink> Links { get; set; }
		[ExpressionRewrite(nameof(Links), nameof(CollectionLink.Child))]
		public override ICollection<Show> Shows
		{
			get => Links?.Select(x => x.Child).ToList();
			set => Links = value?.Select(x => new CollectionLink(this, x)).ToList();
		}
		
		[SerializeIgnore] [NotMergable] public virtual ICollection<LibraryLink> LibraryLinks { get; set; }
		
		[ExpressionRewrite(nameof(LibraryLinks), nameof(GenreLink.Child))]
		public override ICollection<Library> Libraries
		{
			get => LibraryLinks?.Select(x => x.Library).ToList();
			set => LibraryLinks = value?.Select(x => new LibraryLink(x, this)).ToList();
		}
		
		public CollectionDE() {}

		public CollectionDE(Collection collection)
		{
			Utility.Assign(this, collection);
		}
	}
}