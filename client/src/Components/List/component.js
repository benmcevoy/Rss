import "./style.css"
import React from "react";
import AppContext from "../../AppContext";

export default class ListView extends React.Component {
	static contextType = AppContext;

	constructor(props) {
		super(props);
	}

	render() {
		const viewModel = this.props.viewModel;
		const ctx = this.context;

		if (ctx.current.item !== null) return null;

		if (viewModel === null) {
			return (<div class="component" id="list"><h3>Loading...</h3></div>);
		}

		return (
			<div class="component" id="list">
				<div class="header">
					<h3>{viewModel.name || "List"}</h3>
					<button onClick={() => ctx.refresh(viewModel.type, viewModel.id)}>Refresh</button>
					<button onClick={() => ctx.markAsRead(viewModel.type, viewModel.id)}>Mark as read</button>
				</div>
				<nav>
					<ul class="items">
						{viewModel.rssItems
							.map(item =>
								<div class="item">
									<div>
										<a onClick={() => ctx.updateCurrent(item.folderId, item.feedId, item.id)}>{item.name}</a>
										<div>{item.snippet}</div>
									</div>
									<sub class="tagline">
										<a onClick={() => ctx.updateCurrent(item.folderId, item.feedId)}>{item.feedName}</a> <span>({item.publishedDateTime})</span>
									</sub>
								</div>)}
					</ul>
				</nav>
			</div>
		);
	}
}
