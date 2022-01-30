<template>
    <tr>
        <td>
            {{item.id}}
        </td>
        <td>
            {{item.name}}
        </td>
        <td>
            <select v-model="item.categoryGroupId">
                <option value="null">None</option>
                <option v-for="group in categoryGroups"
                :key="group.id"
                :value="group.id">
                {{group.name}}
                </option>
            </select>
        </td>
        <td>
            <button class="button">

            </button>
        </td>
    </tr>
</template>

<script>
import { mapState } from 'vuex'

export default {
  data() {
    return {
    }
  },
  props: {
    isNew: {
        type: Boolean,
        default: false,
    },
    item: {
        type: Object,
        required: true,
    },
  },
  computed: {
    ...mapState(['categoryGroups']),
  },
  methods: {
    onDelete() {

    }
  }
}
</script>