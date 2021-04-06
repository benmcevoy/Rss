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
		this.onFeedSelected = this.onFeedSelected.bind(this);
		// this.context appears not to be available here, so use componentDidMount
	}

	onFeedSelected(e, item) {
		e.preventDefault();
		this.context.updateCurrent(item.folderId, item.feedId, null);
	}

	fetchData() {
		const ctx = this.context.current;
		let url, id;

		if (ctx.item !== null) {
			id = ctx.item;
			url = Config.api.item.read+ `/${id}`;
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

		if(this.context.current.item === null) return null;

		if (viewModel === null) {
			return (<div class="component" id="item"><h3>Loading...</h3></div>);
		}

		return (
			<div class="component" id="item">
				<div class="header">
					<h3><a href="#" onClick={(e) => this.onFeedSelected(e, viewModel)}>{viewModel.feedName || "List"}</a></h3>
				</div>
				<div class="body">
					<h5><a href="{viewModel.linkUrl}" target="_blank">{viewModel.name}</a></h5>
					<sub>{viewModel.publishedDateTime}</sub>
					{/* i also like to live dangerously */}
					<div dangerouslySetInnerHTML={{__html: viewModel.content}} ></div>
				</div>
			</div>
		)
	}
}
