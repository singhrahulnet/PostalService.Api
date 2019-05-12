using PostalService.Api.Domain;
using System.Collections.Generic;
using Xunit;


namespace PostalService.Test.Unit
{
    public class ParcelRuleProcessorTest 
    {       
        public static IEnumerable<object[]> GetInputParams()
        {
            // THE TEST DATA STRUCTURE
            // ParcelRuleCollection rules, int firstPriority

            //Already ordered
            yield return new object[] { new ParcelRuleCollection { ParcelRules = new List<ParcelRule> {
                                                               new ParcelRule { Priority = 1 },
                                                               new ParcelRule { Priority = 2 },
                                                               new ParcelRule { Priority = 3 },
                                                               new ParcelRule { Priority = 4 },
            } }, 1 };

            //Un-ordered
            yield return new object[] { new ParcelRuleCollection { ParcelRules = new List<ParcelRule> {
                                                               new ParcelRule { Priority = 4 },
                                                               new ParcelRule { Priority = 2 },
                                                               new ParcelRule { Priority = 1 },
                                                               new ParcelRule { Priority = 3 },
            } }, 1 };

            //non-sequential
            yield return new object[] { new ParcelRuleCollection { ParcelRules = new List<ParcelRule> {
                                                               new ParcelRule { Priority = 40 },
                                                               new ParcelRule { Priority = 20 },
                                                               new ParcelRule { Priority = 10 },
                                                               new ParcelRule { Priority = 30 },
            } }, 10 };

            //single value
            yield return new object[] { new ParcelRuleCollection { ParcelRules = new List<ParcelRule> {
                                                               new ParcelRule { Priority = 40 }
            } }, 40 };
        }

        [Theory(DisplayName = "ParcelRuleProcessor: FirstRule Returns Correct First Rule Handler")]
        [MemberData(nameof(GetInputParams))]
        public void FirstRule_returns_correct_firstHandler(ParcelRuleCollection rules, int firstPriority)
        {
            //Given
            var sut = new ParcelRuleProcessor(rules);

            //When
            var actual = sut.FirstRule;

            //Then
            Assert.IsAssignableFrom<ParcelRuleBase>(actual);
            Assert.Equal(firstPriority, actual.Priority);
        }
    }
}
