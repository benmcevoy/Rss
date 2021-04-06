import "./style.css"
import React from "react";
import Config from "../../config";
import AppContext from "../../AppContext";

export default class ItemView extends React.Component {
	static contextType = AppContext;

	constructor(props) {
		super(props);
		this.state = {
			viewModel: null,
			id: null
		};

		this.fetchData = this.fetchData.bind(this);
		this.onItemSelected = this.onItemSelected.bind(this);
		this.onFeedSelected = this.onFeedSelected.bind(this);
		// this.context appears not to be available here, so use componentDidMount
	}

	onItemSelected(e, item) {
		e.preventDefault();
		this.context.updateCurrent(item.folderId, item.feedId, item.id);
	}

	onFeedSelected(e, item) {
		e.preventDefault();
		this.context.updateCurrent(item.folderId, item.feedId, null);
	}

	// TODO: as I continue refactoring this will IoC it's way to the top of the DOM...
	fetchData() {
		const ctx = this.context.current;
		let url, id;

		if (ctx.folder === null && ctx.feed === null) {
			id = "stream";
			url = Config.api.stream.get;
		}

		if (ctx.folder !== null && ctx.feed === null) {
			id = ctx.folder;
			url = Config.api.folder.get + `/${id}`;
		}

		if (ctx.folder !== null && ctx.feed !== null) {
			id = ctx.feed;
			url = Config.api.feed.get + `/${id}`;
		}

		// unchanged
		if (id === this.state.id) return;

		fetch(url)
			.then(x => x.json())
			.then(x => this.setState({ viewModel: x, id: id }));
	}

	componentDidMount = () => this.fetchData();
	componentDidUpdate = () => this.fetchData();

	render() {
		const viewModel = this.state.viewModel;

		if (viewModel === null) {
			return (<div class="component" id="list"><h3>Loading...</h3></div>);
		}

		return (
			<div class="component" id="list">
				<div class="header">
					<h3>{viewModel.name || "List"}</h3>
					<span>TODO: found in folder etc</span>
				</div>
				<nav>
					<ul class="items">
						{viewModel.rssItems
							.map(item => 
									<div class="item">
										<div>
											<a href="#" onClick={(e) => this.onItemSelected(e, item)}>{item.name}</a>
											<div>{item.snippet}</div>
										</div>
										<sub class="tagline">
											<a href="#" onClick={(e) => this.onFeedSelected(e, item)}>{item.feedName}</a> <span>{item.publishedDateTime}</span>
										</sub>
									</div>
							)}
					</ul>
				</nav>
			</div>
		)
	}
}
