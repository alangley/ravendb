﻿namespace Raven.Studio.Commands
{
	using System.ComponentModel.Composition;
	using Caliburn.Micro;
	using Features.Database;
	using Messages;
	using Plugin;

	public class DeleteDocument
	{
		readonly IEventAggregator events;
		readonly IServer server;

		[ImportingConstructor]
		public DeleteDocument(IServer server, IEventAggregator events)
		{
			this.server = server;
			this.events = events;
		}

		public void Execute(DocumentViewModel document)
		{
			using (var session = server.OpenSession())
			{
				session.Advanced.AsyncDatabaseCommands
					.DeleteDocumentAsync(document.Id);
			}

			events.Publish(new StatisticsUpdateRequested());
			events.Publish(new DocumentDeleted(document));
		}
	}
}