namespace PassStorageService.CORE
{  
    public class Parameters
    {
        //Add any relevant defaults here
        public readonly int     MinNumOfIterationsParm  = 10000;
        public readonly string  FileFolder              = "/PASSKEEP/STORE/";
        
        public int MinNumOfIterations()
        {
            return MinNumOfIterationsParm;
        }
    }
}
