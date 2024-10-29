<template>
    <div class="sticky-note" :class="{ completed: todo.IsCompleted }">
        <h3>{{ todo.title }}</h3>
        <button class="mark-complete" @click="toggleComplete">
            {{ todo.isCompleted ? 'Undo'  : 'Complete' }}
        </button>
        <button class="delete" @click="$emit('delete-todo', todo.id)">❌</button>
    </div>
</template>

<script>
export default {
    props: {
        todo: {
            type: Object,
            required: true,
        },
    },
    methods: {
        toggleComplete() {
            this.$emit('toggle-complete', this.todo);
        },
    },
};
</script>

<style scoped>
.sticky-note {
    color: black;
  width: 200px;
  height: 200px;
  background-color: #fef68a; /* Default sticky note color */
  margin: 10px;
  padding: 15px;
  border: 2px solid #fcd34d;
  border-radius: 10px;
  box-shadow: 3px 3px 8px rgba(0, 0, 0, 0.2), -3px -3px 8px rgba(0, 0, 0, 0.1);
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  transition: transform 0.2s, background-color 0.3s;
  cursor: grab;
  position: relative;
}

.sticky-note.complete {
    background-color: #d1fae5;
}

.sticky-note:hover {
  transform: rotate(-2deg) scale(1.05);
}

button {
    border: none;
    cursor: pointer;
    background: none;
}

.mark-complete {
    color: #059669;
}

.delete {
    color: red;
    align-self: flex-end;
}

</style>