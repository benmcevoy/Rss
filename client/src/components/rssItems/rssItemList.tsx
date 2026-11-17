import styles from './rssItems.module.css'
import type { RssItemList } from "../../api/rss"
import { Refresh, MarkAsRead } from '../../services/service'

export function RssItemList({ data, onChange }: { data: RssItemList | null, onChange: VoidFunction }) {
    if (data === null) return <></>;

    return <>
        <div className={styles.rssItems}>
            <h3>
                {data.name}
                <div>
                    <button onClick={() => MarkAsRead(location.pathname).then(onChange)}>Mark all as read</button>
                    <button onClick={() => Refresh(location.pathname).then(onChange)}>Refresh</button>
                </div>
            </h3>

            <ul>
                {data.rssItems?.map(article => {
                    const feed = data.feeds?.find(x => x.id == article.feedId);
                    return <li key={article.id}>

                        <a className={styles.title} href={`/article/${article.id}`}>{article.name}</a>
                        <div className={styles.meta}>
                            <button onClick={() => MarkAsRead(`/article/${article.id}`).then(onChange)}>Mark as read</button>
                            <a href={`/feed/${article.feedId}`}><img className="favIcon" src={feed?.favIcon ?? "/icon-rss.png"} /> {feed?.name}</a>
                            {article.publishedDateTime && (
                                <time dateTime={article.publishedDateTime}>
                                    {new Date(article.publishedDateTime).toLocaleDateString('en-AU', {
                                        weekday: 'long', day: '2-digit', month: 'long', year: 'numeric',
                                    })}
                                </time>
                            )}
                        </div>
                        <p className={styles.snippet}>{article.snippet}</p>
                    </li>
                })}
            </ul>
        </div>
    </>
}

