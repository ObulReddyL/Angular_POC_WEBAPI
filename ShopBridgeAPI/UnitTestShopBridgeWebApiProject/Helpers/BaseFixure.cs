using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UnitTestShopBridgeWebApiProject.Helpers
{
    class BaseFixure
    {
    }


    public class DbSetBuilder<T> where T : class
    {
        public static DbSetBuilder<T> Create()
        {
            return new DbSetBuilder<T>();
        }

        private List<T> seedList;



        public DbSetBuilder<T> WithItems(IQueryable<T> seedListPartial, params T[] args)
        {
            var seedList = new List<T>(seedListPartial);
            seedList.AddRange(args);

            this.seedList = seedList;
            return this;
        }

        public DbSetBuilder<T> WithAllItems(params T[] args)
        {
            var seedList = new List<T>();
            seedList.AddRange(args);

            this.seedList = seedList;
            return this;

        }

        public DbSet<T> Build()
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(seedList.AsQueryable().Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(seedList.AsQueryable().Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(seedList.AsQueryable().ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(seedList.AsQueryable().GetEnumerator());

            return mockSet.Object;
        }

        public DbSet<T> BuildDbAsyncEnumerable()
        {
            var mockSet = new Mock<DbSet<T>>();

            mockSet.As<IDbAsyncEnumerable<T>>().Setup(m => m.GetAsyncEnumerator()).Returns(new TestDbAsyncEnumerator<T>(seedList.GetEnumerator()));
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(new TestDbAsyncQueryProvider<T>(seedList.AsQueryable().Provider));
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(seedList.AsQueryable().Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(seedList.AsQueryable().ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(seedList.GetEnumerator());

            return mockSet.Object;
        }

    }
    public class DbSetData<T> where T : class
    {
        public static DbSetData<T> Start()
        {
            return new DbSetData<T>();
        }

        public DbSet<T> BuildData(T arg)
        {
            return DbSetBuilder<T>
                .Create()
                .WithItems(new List<T>()
                {
                    arg
                }.AsQueryable())
                .Build();
        }

        public DbSet<T> BuildDataUsingDbAsyncEnumerable(params T[] arg)
        {
            return DbSetBuilder<T>
                .Create()
                .WithAllItems(arg)
                .BuildDbAsyncEnumerable();
        }

    }

    internal class TestDbAsyncQueryProvider<TEntity> : IDbAsyncQueryProvider
    {
        private readonly IQueryProvider _inner;

        internal TestDbAsyncQueryProvider(IQueryProvider inner)
        {
            _inner = inner;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return new TestDbAsyncEnumerable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new TestDbAsyncEnumerable<TElement>(expression);
        }

        public object Execute(Expression expression)
        {
            return _inner.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return _inner.Execute<TResult>(expression);
        }

        public Task<object> ExecuteAsync(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute(expression));
        }

        public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute<TResult>(expression));
        }
    }

    internal class TestDbAsyncEnumerable<T> : EnumerableQuery<T>, IDbAsyncEnumerable<T>, IQueryable<T>
    {
        public TestDbAsyncEnumerable(IEnumerable<T> enumerable)
            : base(enumerable)
        { }

        public TestDbAsyncEnumerable(Expression expression)
            : base(expression)
        { }

        public IDbAsyncEnumerator<T> GetAsyncEnumerator()
        {
            return new TestDbAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
        {
            return GetAsyncEnumerator();
        }

        IQueryProvider IQueryable.Provider
        {
            get { return new TestDbAsyncQueryProvider<T>(this); }
        }
    }

    internal class TestDbAsyncEnumerator<T> : IDbAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        public TestDbAsyncEnumerator(IEnumerator<T> inner)
        {
            _inner = inner;
        }

        public void Dispose()
        {
            _inner.Dispose();
        }

        public Task<bool> MoveNextAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_inner.MoveNext());
        }

        public T Current
        {
            get { return _inner.Current; }
        }

        object IDbAsyncEnumerator.Current
        {
            get { return Current; }
        }
    }
}
