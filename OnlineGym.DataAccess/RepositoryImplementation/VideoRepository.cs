using OnlineGym.DataAccess.Data;
using OnlineGym.Entities.Models;
using OnlineGym.Entities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGym.DataAccess.RepositoryImplementation
{
	public class VideoRepository : GenericRepository<Video>, IVideoRepository
	{

		private readonly OnlineGymContext _context;
		public VideoRepository(OnlineGymContext context) : base(context)
		{

			_context = context;

		}
		public void Update(Video video)
		{
			var videoInDb = _context.Videos.FirstOrDefault(x => x.Id == video.Id);

			if (videoInDb != null)
			{
				videoInDb.Url = video.Url;
				videoInDb.Title = video.Title;
				videoInDb.category = video.category;
			
			}
		}
	}
}
