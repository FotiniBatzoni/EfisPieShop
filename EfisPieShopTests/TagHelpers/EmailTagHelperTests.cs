using EfisPieShop.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EfisPieShopTests.TagHelpers
{
    public class EmailTagHelperTests
    {
        [Fact]
        public void Generate_Email_Link()
        {
            //Arrange
            EmailTagHelper emailTagHelper = new EmailTagHelper()
            {
                Address = "test@efispieshop.com",
                Content = "Email"
            };

            var tagHelperContext = new TagHelperContext(
                new TagHelperAttributeList(),
                new Dictionary<object, object>(), string.Empty);

            var content = new Mock<TagHelperContent>();

            var tagHelperOutput = new TagHelperOutput(
                "a", 
                new TagHelperAttributeList(),
                (cache,encoder) => Task.FromResult(content.Object));
                
            //Act
            emailTagHelper.Process(tagHelperContext, tagHelperOutput);

            //Assert
            Assert.Equal("Email", tagHelperOutput.Content.GetContent());
            Assert.Equal("a", tagHelperOutput.TagName);
            Assert.Equal("mailto:test@efispieshop.com", tagHelperOutput.Attributes[0].Value);
        }

    }
}
