{
    items(${ARGS})
    {
        id,
        name,
        shortName,
        updated,
        width,
        height,
        wikiLink,
        gridImageLink,
        basePrice,
        lastLowPrice,
        sellFor {
            vendor {
                name,
            }
            currency,
            price,
            priceRUB
        }
        buyFor {
            vendor {
                name
            },
            currency,
            price,
            priceRUB
        }
        usedInTasks {
            id,
        }
    }
}