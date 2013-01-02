using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VersionOne.SDK.ObjectModel;
using VersionOne.SDK.ObjectModel.Filters;

namespace VersionOne.FilterWorkitemsOnEpic
{
    [TestClass]
    public class TestFilterWorkitemsOnEpic
    {
        [TestMethod]
        public void TestCalcPrimaryWorkitemsWeights()
        {
            const string url = "https://www14.v1host.com/v1sdktesting/";
            const string username = "admin";
            const string password = "admin";
            var v1 = new V1Instance(url, username, password);
            var project = v1.Get.ProjectByID("Scope:0");
            var newStory = CreateStoryWithEpic(v1, project);
            var primaryWorkitems = new List<PrimaryWorkitem>(GetFilteredPrimaryWorkitemsForProject(project));
            CollectionAssert.DoesNotContain(primaryWorkitems, newStory);
        }

        private static Story CreateStoryWithEpic(V1Instance v1, Project project)
        {
            var newEpic = v1.Create.Epic(GetName("Epic"), project);
            var attributes = new Dictionary<string, object> {{"Super", newEpic.ID.Token}};
            var newStory = v1.Create.Story(GetName("Story"), project, attributes);
            return newStory;
        }

        private static string GetName(string type)
        {
            return new StringBuilder(type + " " + DateTime.Now).ToString();
        }

        private static ICollection<PrimaryWorkitem> GetFilteredPrimaryWorkitemsForProject(Project project)
        {
            var filter = new PrimaryWorkitemFilter();
            filter.Epic.Add(null);
            var primaryWorkitems = project.GetPrimaryWorkitems(filter);
            return primaryWorkitems;
        } 

        /// <summary> 
        /// go over the primanry work items which does not have parent epic - and calc grade weights
        /// </summary> 0 
        /// <param name="project"></param> 
        private void CalcPrimaryWorkitemsWeights(Project project)
        {
            ICollection<PrimaryWorkitem> stories = GetFilteredPrimaryWorkitemsForProject(project);

            if (stories.Count == 0)
            {
                Console.WriteLine("Project {0} does not contain any items", project.Name);
                return;
            }

            foreach (PrimaryWorkitem story in stories)
            {
                //need help - if has parent - skip
/*
                if (_configData.debugMode && story.DisplayID != "D-02289")
                    continue;

                if (!CalcCeilingValue(story, null, "_CurrentFYRevenues", "_CurrentFYRevenuesGrade", 1000000))
                    continue;

                if (!CalcCeilingValue(story, null, "_FutureRevenuePotential", "_FutureRevenuePotentialGrade", 10000000))
                    continue;

                story.Save();
*/
            }
        }

    }
}
