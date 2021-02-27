using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Kyoo.Models.Attributes;

namespace Kyoo.Models
{
	public class PeopleRole : IResource
	{
		[SerializeIgnore] public int ID { get; set; }
		[SerializeIgnore] public int PeopleID { get; set; }
		[SerializeIgnore] public virtual People People { get; set; }
		
		[ExpressionRewrite(nameof(People) + "." + nameof(Models.People.Slug))]
		public string Slug
		{
			get => People.Slug;
			set => People.Slug = value;
		}
		
		[ExpressionRewrite(nameof(People) + "."+ nameof(Models.People.Name))]
		public string Name
		{
			get => People.Name;
			set => People.Name = value;
		}
		
		[ExpressionRewrite(nameof(People) + "."+ nameof(Models.People.Poster))]
		public string Poster
		{
			get => People.Poster;
			set => People.Poster = value;
		}
		
		[ExpressionRewrite(nameof(People) + "."+ nameof(Models.People.ExternalIDs))]
		public ICollection<MetadataID> ExternalIDs
		{
			get => People.ExternalIDs;
			set => People.ExternalIDs = value;
		}

		[SerializeIgnore] public int ShowID { get; set; }
		[SerializeIgnore] public virtual Show Show { get; set; }
		public string Role { get; set; }
		public string Type { get; set; }

		public PeopleRole() {}
		
		public PeopleRole(People people, Show show, string role, string type)
		{
			People = people;
			Show = show;
			Role = role;
			Type = type;
		}

		public PeopleRole(string slug, 
			string name, 
			string role, 
			string type,
			string poster,
			IEnumerable<MetadataID> externalIDs)
		{
			People = new People(slug, name, poster, externalIDs);
			Role = role;
			Type = type;
		}
	}

	public class ShowRole : IResource
	{
		public int ID { get; set; }
		public string Role { get; set; }
		public string Type { get; set; }

		public string Slug { get; set; }
		public string Title { get; set; }
		public ICollection<string> Aliases { get; set; }
		[SerializeIgnore] public string Path { get; set; }
		public string Overview { get; set; }
		public Status? Status { get; set; }
		public string TrailerUrl { get; set; }
		public int? StartYear { get; set; }
		public int? EndYear { get; set; }
		public string Poster { get; set; }
		public string Logo { get; set; }
		public string Backdrop { get; set; }
		public bool IsMovie { get; set; }
		
		public ShowRole() {}

		public ShowRole(PeopleRole x)
		{
			ID = x.ID;
			Role = x.Role;
			Type = x.Type;
			Slug = x.Show.Slug;
			Title = x.Show.Title;
			Aliases = x.Show.Aliases?.ToArray();
			Path = x.Show.Path;
			Overview = x.Show.Overview;
			Status = x.Show.Status;
			TrailerUrl = x.Show.TrailerUrl;
			StartYear = x.Show.StartYear;
			EndYear = x.Show.EndYear;
			Poster = x.Show.Poster;
			Logo = x.Show.Logo;
			Backdrop = x.Show.Backdrop;
			IsMovie = x.Show.IsMovie;
		}

		public static Expression<Func<PeopleRole, ShowRole>> FromPeopleRole => x => new ShowRole
		{
			ID = x.ID,
			Role = x.Role,
			Type = x.Type,
			Slug = x.Show.Slug,
			Title = x.Show.Title,
			Aliases = x.Show.Aliases != null ? x.Show.Aliases.ToArray() : null,
			Path = x.Show.Path,
			Overview = x.Show.Overview,
			Status = x.Show.Status,
			TrailerUrl = x.Show.TrailerUrl,
			StartYear = x.Show.StartYear,
			EndYear = x.Show.EndYear,
			Poster = x.Show.Poster,
			Logo = x.Show.Logo,
			Backdrop = x.Show.Backdrop,
			IsMovie = x.Show.IsMovie
		};
	}
}