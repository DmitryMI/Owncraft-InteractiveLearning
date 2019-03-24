using InteractiveLearning.Core;

namespace InteractiveLearning.NetworkInteraction
{
    class Networker
    {

        #region Singleton
        // Singleton --------------------------
        private static Networker _instance;

        public static Networker GetInstance()
        {
            if(_instance == null)
                _instance = new Networker();
            return _instance;
        }

        //-------------------------------------
#endregion

        

        private Networker()
        {
            
        }

        public Category ReadDataFromServer()
        {
            // TODO Retreiving task list from tutor's server

            // Demo placeholder
            Category rootCategory = new Category();
            rootCategory.Name = "ROOT";
            rootCategory.Description = "Root category should not be displayed to user";
            Category integralsSubCat = new Category();

            // Integrals demo
            integralsSubCat.Name = "Integrals";
            integralsSubCat.Description = "Various exercises related to integral equations";
            rootCategory.Add(integralsSubCat);

            // Quadratic equations demo
            Category qeqSubCat = new Category();
            qeqSubCat.Name = "Quadratic equations";
            qeqSubCat.Description = "Quadratic equations resolving";
            rootCategory.Add(qeqSubCat);

            // Derivatives demo
            Category derivativesSubCat = new Category();
            derivativesSubCat.Name = "Derivatives";
            derivativesSubCat.Description = "Some exercises about derivatives";
            rootCategory.Add(derivativesSubCat);

            return rootCategory;
        }

        
    }
}
