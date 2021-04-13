import "./style.css"
import React, { useState, useEffect } from "react";
import { useAppContext } from "../../AppContext";

export default function AddFeed(props) {
	const ctx = useAppContext();
	let [feedUrl, setFeedUrl] = useState("");
	let [folderId, setFolderId] = useState("");

	useEffect(() => { setFolderId(props.folderId) }, [props]);

	if (!props.isVisible) return null;

	// TODO: in an ideal world build out a little forms library, or use one off the shelf
	// validation would be nice
	return (
		<div class="component form" id="add-feed">
			<div class="header">
				<h3>Add new feed</h3>
			</div>
			<div class="body">
				<div class="field">
					<label for="feedUrl">Feed RSS/ATOM url:</label>
					<input type="text" name="feedUrl" value={feedUrl} onChange={e => setFeedUrl(e.target.value)} />
				</div>
				<div class="field">
					<label for="folder">Folder:</label>
					<span>{props.folder}</span>
				</div>
				<div class="field">
					<button name="add" onClick={e => ctx.subscribeSave({ feedUrl, folderId })}>Save</button>
				</div>
			</div>
		</div>
	)
}
