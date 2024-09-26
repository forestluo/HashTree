namespace SimpleTeam.Container.File
{
    internal class PageType
    {
        //Head Page.
        public const int HEAD_PAGE = 1;
        //Free Page
        public const int FREE_PAGE = 2;
        //Data Page
        public const int DATA_PAGE = 3;
        //Queue Page
        public const int QUEUE_PAGE = 4;
        //Queue Element
        public const int QUEUE_ELEMENT = 5;
        //Index Page
        public const int INDEX_PAGE = 6;
        //Index Element
        public const int INDEX_ELEMENT = 7;

        public static bool IsValid(int value)
        {
            //Return result.
            return value >= 1 && value <= 7;
        }
    }
}
