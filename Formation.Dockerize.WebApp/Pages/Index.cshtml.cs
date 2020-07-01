using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Distributed;

namespace Formation.Dockerize.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IDistributedCache  _cache;

        [BindProperty]
        public string Vote { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IDistributedCache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        public void OnGet()
        {
            GetVotes();
        }

        public void OnPost()
        {
            if (this.Vote == "Reset")
            {
                ResetVotes();
            }
            else
            {
                VoteFor();
            }

            GetVotes();
        }

        public void ResetVotes()
        {
            _cache.SetString("Chien", "0");
            _cache.SetString("Chat", "0");
        }

        private void VoteFor()
        {
            int value1 = Convert.ToInt32(_cache.GetString(this.Vote));

            value1 = value1 + 1;

            _cache.SetString(this.Vote, value1.ToString());
        }

        private void GetVotes()
        {
            int value1 = Convert.ToInt32(_cache.GetString("Chien"));
            int value2 = Convert.ToInt32(_cache.GetString("Chat"));

            ViewData["Chien"] = value1;
            ViewData["Chat"] = value2;
        }
    }
}