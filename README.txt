Html Helper Extension for ImageLink to generate <a href="..."><image src="..." alt="..." /></a>

Should offer similar overloads to ActionLink().  Eventually may grow to support lambda based options.

Examples:

<%= Html.ImageLink("image.png", "alt text", "About") %>

<%= Html.ImageLink("image.png", "alt text", "About", new {}, new {id = "about-link"}, new {id = "image-link"}) %>