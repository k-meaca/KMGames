using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        //----------PROPERTIES----------//

        private readonly KmGamesDBContext _dbContext;

        //----------CONSTRUCTOR----------//

        public UnitOfWork(KmGamesDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        //----------METHODS----------//
        public void SaveChanges()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.ToList()
                    .ForEach(entry =>
                        entry.Reload()
                    );

                throw new Exception("Data modified o deleted by other user.");
            }
            catch(Exception)
            {
                throw new Exception("Something wrong ocurred.");
            }
        }
    }
}
