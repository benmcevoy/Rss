import "./style.css"
import React from "react";
import { useAppContext } from "../../AppContext";

export default function ListView(props) {
	const ctx = useAppContext();
	const viewModel = props.viewModel;

	if (ctx.currentItem !== null) return null;

	if (viewModel === null) {
		return (<div class="component" id="list"><h3>Loading...</h3></div>);
	}

	return (
		<div class="component" id="list">
			<div class="header">
				<h3>{viewModel.name || "List"}</h3>
				<button onClick={() => ctx.refresh(viewModel.type, viewModel.id)}>Refresh</button>
				<button onClick={() => ctx.markAsRead(viewModel.type, viewModel.id)}>Mark as read</button>
				{
				viewModel.type === "Feed" &&
					<button onClick={() => ctx.unsubscribe(viewModel.id)}>Unsubscribe</button>
				}
				{
				viewModel.type === "Folder" &&
					<button onClick={() => ctx.subscribe(viewModel.id)}>Add feed</button>
				}
			</div>
			<nav>
				<ul class="items">
					{viewModel.rssItems
						.map(item =>
							<div class="item">
								<div>
									<button class="link-button" onClick={() => ctx.updateCurrent(item.folderId, item.feedId, item.id)}>{item.name}</button>
									<div>{item.snippet}</div>
								</div>
								<sub class="tagline">
									<button class="link-button" onClick={() => ctx.updateCurrent(item.folderId, item.feedId)}>{item.feedName}</button> <span>({item.publishedDateTime})</span>
								</sub>
							</div>)}
				</ul>
			</nav>
		</div>
	);
}

