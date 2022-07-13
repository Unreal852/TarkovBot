{
    tasks(${ARGS})
{
    id,
    name,
    trader {
        id
    },
    map {
        id
    },
    experience,
    wikiLink,
    minPlayerLevel,
    objectives {
        id,
        type,
        description,
        maps {
            id
        },
        optional,
        __typename ... on TaskObjectiveBuildItem {
            item {
                id
            }
            containsAll {
                id
            }
            containsOne {
                id
            }
            attributes {
                name,
                requirement {
                    value,
                    compareMethod
                }
            }
        }
        __typename ... on TaskObjectiveExperience {
            healthEffect {
                bodyParts,
                effects,
                time {
                    compareMethod,
                    value
                }
            }
        }
        __typename ... on TaskObjectiveExtract {
            exitStatus,
            zoneNames
        }
        __typename ... on TaskObjectiveMark {
            markerItem {
                id
            }
        }
        __typename ... on TaskObjectivePlayerLevel {
            playerLevel
        }
        __typename ... on TaskObjectiveItem {
            item {
                id
            },
            count,
            foundInRaid,
            dogTagLevel,
            maxDurability,
            minDurability
        }
    }
}
}