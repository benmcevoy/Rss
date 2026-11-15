import DOMPurify from 'dompurify'
import styles from './rssItems.module.css'
import type { Article } from "../../api/rss"

export function RssItem({ data }: { data: Article | null }) {
    if (data === null) return <></>;

    return <>
        <div className={styles.rssItems}>
            <a className={styles.title} href={data.url ?? `/article/${data.id}`}>{data.name}</a>

            <div className={styles.meta}>
                <img className="favIcon" src={data?.feedFavIcon ?? "/icon-rss.png"} /> <a href={data.feedWebsiteUrl ?? `/feed/${data.feedId}`}>{data.feedName}</a>
                <time dateTime={data.publishedDateTime}>{data.publishedDateTime}</time>
            </div>

            <div dangerouslySetInnerHTML={{ __html: DOMPurify.sanitize(data.content ?? "") }} />
        </div>
    </>
}

