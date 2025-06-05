using System;
using JobCounselor.Domain.ValueObjects;
using Xunit;

namespace JobCounselor.Tests.Domain;

/// <summary>
/// Unit tests validating domain value object rules.
/// </summary>
public class ValueObjectTests
{
    [Fact]
    public void Skill_NameCannotBeEmpty()
    {
        Assert.Throws<ArgumentException>(() => new Skill("", ""));
    }

    [Fact]
    public void EducationItem_EndDateCannotBeBeforeStartDate()
    {
        var start = new DateTime(2020, 1, 1);
        var end = new DateTime(2019, 1, 1);
        Assert.Throws<ArgumentException>(() => new EducationItem("Degree", "Inst", start, end));
    }

    [Fact]
    public void ExperienceItem_EndDateCannotBeBeforeStartDate()
    {
        var start = new DateTime(2020, 1, 1);
        var end = new DateTime(2019, 1, 1);
        Assert.Throws<ArgumentException>(() => new ExperienceItem("Title", "Comp", start, end, "Desc"));
    }

    [Fact]
    public void Language_NameCannotBeEmpty()
    {
        Assert.Throws<ArgumentException>(() => new Language("", "Basic"));
    }
}
