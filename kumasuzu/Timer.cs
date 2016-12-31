using System;
using System.Threading;
using System.Threading.Tasks;

namespace kumasuzu
{
	public class Timer : CancellationTokenSource, IDisposable
	{
		private static int period;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="action">実行するアクション</param>
		/// <param name="state">継続アクションによって使用されるデータを表すオブジェクト</param>
		/// <param name="dueTime">最初の実行までに遅延する時間（ミリ秒）</param>
		/// <param name="period">アクションを実行する間隔（ミリ秒）</param>
		/// <param name="cts">CancellationTokenSourceオブジェクト</param>
		public Timer(Action<Object> action, object state, int dueTime, int period, CancellationTokenSource cts)
		{
			Timer.period = period;

			Task.Delay(dueTime, cts.Token).ContinueWith(
				async (t, s) =>
				{
					var tuple = (Tuple<Action<Object>, object>)s;

					while (!cts.IsCancellationRequested)
					{
						await Task.Run(() => tuple.Item1(tuple.Item2), cts.Token);
						await Task.Delay(Timer.period);
					}
				},
				Tuple.Create(action, state),
				CancellationToken.None,
				TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnRanToCompletion,
				TaskScheduler.Default
			);
		}

		public void changePeriod(int period)
		{
			Timer.period = period;
		}

		public new void Dispose() { base.Cancel(); }
	}
}
