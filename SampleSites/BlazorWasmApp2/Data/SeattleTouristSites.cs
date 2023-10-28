namespace BlazorWasmApp2.Data;

public class SeattleTouristSites
{
    public static IEnumerable<SiteData> Data { get; } = new SiteData[] {
        new(1, "Space Needle", "The Space Needle is an observation tower in Seattle.", "https://en.wikipedia.org/wiki/Space_Needle"),
        new(2, "Lake Union Park", "Lake Union Park is a 12-acre (4.9 ha) park located at the south end of Lake Union in Seattle.", "https://en.wikipedia.org/wiki/Lake_Union_Park"),
        new(3, "Museum of Pop Culture", "The Museum of Pop Culture, or MoPOP (previously called EMP Museum) is a nonprofit museum dedicated to contemporary popular culture.", "https://en.wikipedia.org/wiki/Museum_of_Pop_Culture"),
    };
}
