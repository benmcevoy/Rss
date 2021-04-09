import "./style.css"
import React from "react";
import {useAppContext} from "../../AppContext";

export default function AddFeed(props) {
	const viewModel = props.viewModel;
	const ctx = useAppContext();

	if (ctx.currentItem === null) return null;

	if (viewModel === null) {
		return (<div class="component" id="item"><h3>Loading...</h3></div>);
	}

	return (
		<div class="component" id="item">
			<div class="header">
				<h3><button class="link-button" onClick={() => ctx.updateCurrent(viewModel.folderId, viewModel.feedId)}>{viewModel.feedName || "Feed"}</button></h3>
			</div>
			<div class="body">
				<h5><a href={viewModel.url} target="_blank" rel="noreferrer">{viewModel.name}</a></h5>
				<sub>{viewModel.publishedDateTime}</sub>
				{/* i also like to live dangerously */}
				<div dangerouslySetInnerHTML={{ __html: viewModel.content }} ></div>
			</div>
		</div>
	)
}
