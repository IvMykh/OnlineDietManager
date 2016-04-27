using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineDietManager.Domain.CoursesManagement;
using OnlineDietManager.Domain.DishesManagement;
using OnlineDietManager.Domain.Repositories;
using OnlineDietManager.Domain.UnitsOfWork;
using OnlineDietManager.WebUI.Repositories;

namespace OnlineDietManager.WebUI.UnitsOfWork
{
    //public class ODMUnitOfWork
    //    : IUnitOfWork,
    //      IDisposable
    //{
    //    private OnlineDietManagerContext _context;
    //    private bool _isDisposed;

    //    IRepository<Ingredient> _ingredientsRepository;
    //    IRepository<DishComponent> _dishComponentsRepository;
    //    IRepository<Dish> _dishesRepository;
    //    //IRepository<User> _usersRepository;
    //    IRepository<Meal> _mealsRepository;
    //    IRepository<Day> _daysRepository;
    //    IRepository<Course> _coursesRepository;

    //    public ODMUnitOfWork()
    //    {
    //        this._context = new OnlineDietManagerContext();
    //        this._isDisposed = false;
    //    }

    //    public IRepository<Ingredient> IngredientsRepository
    //    {
    //        get
    //        {
    //            if (_ingredientsRepository == null)
    //            {
    //                _ingredientsRepository = new GenericRepository<Ingredient>(this._context);
    //            }

    //            return _ingredientsRepository;
    //        }
    //    }

    //    public IRepository<DishComponent> DishComponentsRepository
    //    {
    //        get
    //        {
    //            if (_dishComponentsRepository == null)
    //            {
    //                _dishComponentsRepository = new GenericRepository<DishComponent>(this._context);
    //            }

    //            return _dishComponentsRepository;
    //        }
    //    }

    //    public IRepository<Dish> DishesRepository
    //    {
    //        get
    //        {
    //            if (_dishesRepository == null)
    //            {
    //                _dishesRepository = new GenericRepository<Dish>(this._context);
    //            }

    //            return _dishesRepository;
    //        }
    //    }

    //    //public IRepository<User> UsersRepository
    //    //{
    //    //    get
    //    //    {
    //    //        if (_usersRepository == null)
    //    //        {
    //    //            _usersRepository = new Repository<User>(this._context);
    //    //        }
    //    //
    //    //        return _usersRepository;
    //    //    }
    //    //}

    //    public IRepository<Meal> MealsRepository
    //    {
    //        get
    //        {
    //            if (_mealsRepository == null)
    //            {
    //                _mealsRepository = new GenericRepository<Meal>(this._context);
    //            }

    //            return _mealsRepository;
    //        }
    //    }

    //    public IRepository<Day> DaysRepository
    //    {
    //        get
    //        {
    //            if (_daysRepository == null)
    //            {
    //                _daysRepository = new GenericRepository<Day>(this._context);
    //            }

    //            return _daysRepository;
    //        }
    //    }

    //    public IRepository<Course> CoursesRepository
    //    {
    //        get
    //        {
    //            if (_coursesRepository == null)
    //            {
    //                _coursesRepository = new GenericRepository<Course>(this._context);
    //            }

    //            return _coursesRepository;
    //        }
    //    }

    //    public void Save()
    //    {
    //        this._context.SaveChanges();
    //    }

    //    protected virtual void Dispose(Boolean disposing)
    //    {
    //        if (!_isDisposed)
    //        {
    //            if (disposing)
    //            {
    //                _context.Dispose();
    //            }
    //        }

    //        _isDisposed = true;
    //    }

    //    public void Dispose()
    //    {
    //        Dispose(true);
    //        GC.SuppressFinalize(this);
    //    }
    //}
}