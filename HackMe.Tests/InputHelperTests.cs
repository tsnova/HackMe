using HackMe.Application.Helpers;

namespace HackMe.Tests
{
    public class InputHelperTests
    {
        [Fact]
        public void HasPotentialXss_iFrame_ReturnsTrue()
        {
            var script = "<iframe width=\"560\" height=\"315\" src=\"https://www.youtube.com/embed/J8O9_ugpDjE?si=wSfMMUklGmdwlvlI\" title=\"YouTube video player\" frameborder=\"0\" allow=\"accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share\" referrerpolicy=\"strict-origin-when-cross-origin\" allowfullscreen></iframe>";

            Assert.True(InputHelper.HasPotentialXss(script));
        }

        [Fact]
        public void HasPotentialXss_Alert_ReturnsTrue()
        {
            var script = "<script>alert('test')</script>";
            Assert.True(InputHelper.HasPotentialXss(script));
        }


        [Fact]
        public void HasPotentialXss_Js_ReturnsTrue()
        {
            var script = "<script type=\"text/javascript\">console.log('hallo')</script>";
            Assert.True(InputHelper.HasPotentialXss(script));
        }

        [Fact]
        public void HasPotentialXss_Img_ReturnsTrue()
        {
            var script = "<img src=\"https://cdn1.byjus.com/wp-content/uploads/blog/2023/03/17131610/STIM_Happy-Baby-Elephant-Running-scaled.jpeg\" />";
            Assert.True(InputHelper.HasPotentialXss(script));
        }
    }
}